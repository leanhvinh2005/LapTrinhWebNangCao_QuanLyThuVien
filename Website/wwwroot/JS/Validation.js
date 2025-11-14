document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector(".loginform, .registeraccountform, .registercardform");
    const formBackground = document.querySelector(".main_login, .main_registeraccount, .main_registercard");
    const inputs = form.querySelectorAll("input");
    const submitButton = form.querySelector('button[type="submit"]');

    function allFieldsValid() {
        let allValid = true;

        inputs.forEach(input => {
            const value = input.value.trim();
            const fieldValid = $(input).valid();

            if (value === "") {
                allValid = false;
            }
            else if (!fieldValid) {
                allValid = false;
            }
        });

        return allValid;
    }

    function updateButtonState() {
        const isValid = allFieldsValid();

        if (isValid) {
            submitButton.disabled = false;
            submitButton.style.backgroundColor = "#1AC09B";
            submitButton.style.color = "white";
            submitButton.style.borderColor = "#1AC09B";
            submitButton.style.cursor = "pointer";
            formBackground.style.backgroundColor = "#1AC09B";
        } else {
            submitButton.disabled = true;
            submitButton.style.backgroundColor = "lightgrey";
            submitButton.style.color = "#1C1C1C";
            submitButton.style.borderColor = "lightgrey";
            submitButton.style.cursor = "not-allowed";
            formBackground.style.backgroundColor = "lightgrey";
        }
    }

    inputs.forEach(input => {
        input.addEventListener("input", () => {
            const value = input.value.trim();

            if (value === "") {
                input.style.borderColor = "#1C1C1C";
            } else {
                const fieldValid = $(input).valid();
                input.style.borderColor = fieldValid ? "#1AC09B" : "red";
            }

            updateButtonState();
        });

        input.addEventListener("blur", () => {
            const value = input.value.trim();
            if (value === "") {
                input.style.borderColor = "#1C1C1C";
            }
        });
    });

    inputs.forEach(input => {
        input.style.borderColor = "#1C1C1C";
    });
    updateButtonState();
});
