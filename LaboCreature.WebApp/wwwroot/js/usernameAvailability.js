'use strict';

const usernameInput = document.getElementById("username");
const usernameValidationMessage = document.getElementById("usernameAvailability");

let typingTimer;

usernameInput.addEventListener("input", () => {

    clearTimeout(typingTimer);

    const username = usernameInput.value.trim();

    usernameValidationMessage.textContent = "";

    if (username.length < 3)
    {
        return;
    }

    typingTimer = setTimeout(async () => {
        try {
            const response = await fetch(
                `/Member/CheckUsername?username=${encodeURIComponent(username)}`
            );

            const data = await response.json();

            usernameValidationMessage.textContent = data.message;
            usernameValidationMessage.className = data.available
                ? "text-success small"
                : "text-danger small";
        }
        catch (error) {
            console.error("Username check failed:", error);

            usernameValidationMessage.textContent = "Unable to check username availability.";

            usernameValidationMessage.className = "text-danger small";
        }
    }, 750);
})
