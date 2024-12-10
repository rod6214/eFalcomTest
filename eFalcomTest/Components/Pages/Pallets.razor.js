const ingresarModal = new bootstrap.Modal("#palletIngresarModal", { backdrop: 'static' });
const extraerModal = new bootstrap.Modal("#palletExtraerModal", { backdrop: 'static' });

export function showIngresarModalJS()
{
    ingresarModal.show();
}

export function hideIngresarModalJS()
{
    ingresarModal.hide();
}

export function showExtraerModalJS() {
    extraerModal.show();
}

export function hideExtraerModalJS() {
    extraerModal.hide();
}
