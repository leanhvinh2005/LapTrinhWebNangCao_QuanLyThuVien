document.addEventListener("DOMContentLoaded", function () {
    const buttons = document.querySelectorAll(".dropmenu_browse");

    buttons.forEach(button => {
        button.addEventListener("click", function (e) {
            e.stopPropagation();

            const content = button.nextElementSibling;
            content.style.display = content.style.display === "block" ? "none" : "block";
        });
    });
});
