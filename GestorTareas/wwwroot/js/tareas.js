document.addEventListener("DOMContentLoaded", () => {

    /* -> Helpers */
    function getAntiForgeryToken() {
        const el = document.querySelector('input[name="__RequestVerificationToken"]');
        return el ? el.value : null;
    }

    async function postFormData(url, formData) {
        const resp = await fetch(url, {
            method: "POST",
            body: formData
        });
        if (!resp.ok) throw new Error(`HTTP ${resp.status}`);
        return resp.json();
    }

    /* -> Eliminación (SweetAlert) */
    const botonesEliminar = document.querySelectorAll(".btn-eliminar");
    botonesEliminar.forEach(btn => {
        btn.addEventListener("click", async (e) => {
            const id = btn.dataset.id;
            if (!id) return;

            const confirm = await Swal.fire({
                title: "¿Eliminar tarea?",
                text: "Esta acción no se puede deshacer.",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: "Sí, eliminar",
                cancelButtonText: "Cancelar",
                reverseButtons: true
            });

            if (!confirm.isConfirmed) return;

            try {
                const form = new FormData();
                form.append("id", id);
                const token = getAntiForgeryToken();
                if (token) form.append("__RequestVerificationToken", token);

                const data = await postFormData(`/Tareas?handler=Eliminar`, form);

                if (data?.success) {
                    await Swal.fire({ title: "Eliminado", icon: "success", timer: 900, showConfirmButton: false });
                    const row = btn.closest("tr");
                    if (row) row.remove();
                } else {
                    throw new Error(data?.message || "No se pudo eliminar la tarea");
                }
            } catch (err) {
                console.error(err);
                Swal.fire("Error", err.message || "Ocurrió un error", "error");
            }
        });
    });

    /* -> Cambio de estado asíncrono (badge clicable + menú mínimo) */
    // Estructura: <span class="badge badge-... js-badge" data-id="1">Pendiente</span>
    const badges = document.querySelectorAll(".js-badge");

    // menú simple creado en el DOM al vuelo
    function crearMenuCambio(tareaId, badgeEl) {
        const menu = document.createElement("div");
        menu.className = "state-menu";
        menu.style.position = "absolute";
        menu.style.zIndex = 9999;
        menu.style.background = "#fff";
        menu.style.border = "1px solid #dbe6f0";
        menu.style.borderRadius = "6px";
        menu.style.boxShadow = "0 4px 12px rgba(16,24,40,0.08)";
        menu.style.padding = "6px";
        menu.style.display = "flex";
        menu.style.flexDirection = "column";
        menu.style.gap = "6px";

        const opciones = [
            { key: "Pendiente", label: "Pendiente" },
            { key: "En progreso", label: "En progreso" },
            { key: "Completada", label: "Completada" },
        ];

        opciones.forEach(opt => {
            const b = document.createElement("button");
            b.className = "btn";
            b.style.padding = "6px 10px";
            b.style.border = "none";
            b.style.borderRadius = "6px";
            b.style.cursor = "pointer";
            b.textContent = opt.label;
            // dentro de crearMenuCambio, reemplaza el fetch/FormData por esta versión:
            b.onclick = async (ev) => {
                ev.stopPropagation();
                try {
                    const form = new FormData();
                    form.append("id", tareaId);
                    form.append("estado", opt.key);

                    const tokenEl = document.querySelector('input[name="__RequestVerificationToken"]');
                    if (tokenEl) form.append("__RequestVerificationToken", tokenEl.value);

                    const resp = await fetch(`/Tareas?handler=CambiarEstado`, {
                        method: "POST",
                        body: form
                    });

                    // si el status no es ok, intenta leer texto y lanzarlo para que lo veas
                    if (!resp.ok) {
                        const txt = await resp.text();
                        throw new Error(`Server responded ${resp.status}: ${txt.substring(0, 300)}`);
                    }

                    // intentar parsear JSON, pero si no es JSON mostrar el texto
                    let data;
                    const text = await resp.text();
                    try {
                        data = JSON.parse(text);
                    } catch (ex) {
                        throw new Error("Respuesta no es JSON: " + text.substring(0, 300));
                    }

                    if (data?.success) {
                        actualizarBadge(badgeEl, opt.key);
                        Swal.fire({ title: "Estado cambiado", icon: "success", timer: 900, showConfirmButton: false });
                    } else {
                        throw new Error(data?.message || "No se pudo cambiar el estado.");
                    }
                } catch (err) {
                    console.error("Error cambiar estado:", err);
                    Swal.fire("Error", err.message || "Ocurrió un error al cambiar estado", "error");
                } finally {
                    if (menu && menu.parentNode) menu.remove();
                }
            };


            menu.appendChild(b);
        });

        // cerrar al hacer click fuera
        const remover = (ev) => {
            if (!menu.contains(ev.target)) {
                menu.remove();
                document.removeEventListener("click", remover);
            }
        };
        document.addEventListener("click", remover);

        return menu;
    }

    function actualizarBadge(badgeEl, estado) {
        // ajustar texto y clases
        badgeEl.textContent = estado;
        badgeEl.classList.remove("badge-pendiente", "badge-enprogreso", "badge-completada");
        if (estado === "Completada") badgeEl.classList.add("badge-completada");
        else if (estado === "En progreso") badgeEl.classList.add("badge-enprogreso");
        else badgeEl.classList.add("badge-pendiente");
    }

    badges.forEach(b => {
        b.addEventListener("click", (ev) => {
            ev.stopPropagation();
            const tareaId = b.dataset.id;
            // si ya hay un menú en el DOM lo quitamos
            const prev = document.querySelector(".state-menu");
            if (prev) prev.remove();

            const menu = crearMenuCambio(tareaId, b);
            document.body.appendChild(menu);

            // posicionar cerca del badge
            const rect = b.getBoundingClientRect();
            menu.style.left = `${rect.left + window.scrollX}px`;
            menu.style.top = `${rect.bottom + window.scrollY + 6}px`;
        });
    });

});
