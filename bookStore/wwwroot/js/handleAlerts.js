
export function handleDelete(controllerName)  {
    document.addEventListener("DOMContentLoaded", function () {
        document.querySelectorAll(".js-delete").forEach((btn) => {
            btn.addEventListener("click", async () => {
                Swal.fire({
                    title: "Are you sure?",
                    text: "You won't be able to revert this!",
                    icon: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#3085d6",
                    cancelButtonColor: "#d33",
                    confirmButtonText: "Yes, delete it!"
                }).then(async (result) => {
                    if (result.isConfirmed) {
                        const id = btn.dataset.id;
                        console.log(id);
                        const response = await fetch(`/${controllerName}/Delete/${id}`, {
                            method: 'POST'
                        });

                        if (response.ok) {
                            btn.closest("tr").remove();
                            Swal.fire({
                                title: "Deleted!",
                                text: "Your Data has been deleted.",
                                icon: "success"
                            });
                        } else {
                            const errorText = await response.text();
                            Swal.fire({
                                icon: "error",
                                title: "Oops...",
                                text: "Something went wrong!",
                                footer: errorText
                            });
                        }
                    }
                });
            });
        });
    });
}

export function handleCreate(controllerName) {
    document.addEventListener("DOMContentLoaded", function () {
        document.querySelectorAll(".js-create").forEach((btn) => {
            btn.addEventListener("click", async (event) => {
                event.preventDefault();

                const form = btn.closest('form');
                const formData = new FormData(form);

                const response = await fetch(`/${controllerName}/Create`, {
                    method: 'POST',
                    body: formData,
                    credentials: 'same-origin'
                });

                if (response.ok) {
                    await Swal.fire({
                        position: "center",
                        icon: "success",
                        title: "Item has been successfully created!",
                        showConfirmButton: false,
                        timer: 1500
                    });

                    console.log("im here!!");
                    window.location.href = `/${controllerName}/Index`;
                } else {
                    const errorText = await response.text();
                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        text: "Something went wrong!",
                        footer: errorText
                    });
                }
            });
        });
    });
}

export function handleUpdate(controllerName) {
    document.addEventListener("DOMContentLoaded", function () {
        document.querySelectorAll(".js-edit").forEach((btn) => {
            btn.addEventListener("click", async (event) => {
                event.preventDefault();

                const form = btn.closest('form');
                const formData = new FormData(form);
                const id = formData.get("Id");

                const response = await fetch(`/${controllerName}/Edit/${id}`, {
                    method: 'POST',
                    body: formData,
                });

                if (response.ok) {
                    Swal.fire({
                        position: "center",
                        icon: "success",
                        title: "Item has been successfully updated!",
                        showConfirmButton: false,
                        timer: 1500
                    }).then(() => window.location.href = `/${controllerName}/Index`)
                } else {
                    const errorText = await response.text();
                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        text: "Something went wrong!",
                        footer: errorText
                    });
                }
            });
        });
    });
}


