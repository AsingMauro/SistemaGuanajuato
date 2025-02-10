window.ShowSwal = (type, message) => {
    if (type === "success") {
        Swal.fire(
            "Correcto!",
            message,
            'success'
        );
    }
    if (type === "error") {
        Swal.fire(
            "Error",
            message,
            'error'
        );
    }
    if (type === "warning") {
        Swal.fire(
            "Advertencia!",
            message,
            'warning'
        );
    }
}