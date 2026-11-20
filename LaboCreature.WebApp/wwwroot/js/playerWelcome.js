'use strict';

const sections = document.querySelectorAll('section');

function showSection(sectionName) {
    sections.forEach(section => {
        section.hidden = section.id !== sectionName;
    })
}

const createPlayerForm = document.getElementById("createPlayerForm");
const giveCoins = document.getElementById("getFirstCoins");

const createPlayerBtn = document.getElementById("createPlayerButton");
const getCoinsBtn = document.getElementById("getFirstCoinsButton");

const createPlayerMessage = document.getElementById("createPlayerMessage");
const getCoinsMessage = document.getElementById("getCoinsMessage");

createPlayerForm.addEventListener("submit", async (event) => {
    event.preventDefault();
    createPlayerBtn.disabled = true;

    try {
        const response = await fetch(createPlayerForm.action,
            {
                method: 'POST',
                body : new FormData(createPlayerForm)
            });

        if (!response.ok) {
            throw new Error("HTTP error: " + response.status);
        }

        const result = await response.json();


        if (result.redirectUrl != null) {
            window.location.href = result.redirectUrl;
            return;
        }

        if (result.success) {
            createPlayerMessage.textContent = "Player successfully created !";
            createPlayerMessage.hidden = false;
            setTimeout(() => showSection('coinsSection'), 2000);
        }
        else {
            createPlayerMessage.textContent = result.message ?? "Something went wrong. Please try again.";
            createPlayerMessage.hidden = false;
            createPlayerBtn.disabled = false;
        }
    } catch (error) {
        createPlayerMessage.textContent = "An error occurred. Please try again.";
        createPlayerMessage.hidden = false;
        createPlayerBtn.disabled = false;

        console.error(error);
    }
})

giveCoins.addEventListener("submit", async (event) => {
    event.preventDefault();
    getCoinsBtn.disabled = true;
    try {
        const response = await fetch(giveCoins.action, {
            method: 'POST',
            body : new FormData(giveCoins)
        });

        if (!response.ok) {
            throw new Error("HTTP error: " + response.status);
        }

        const result = await response.json();

        if (result.redirectUrl != null) {
            window.location.href = result.redirectUrl;
            return;
        }

        if (result.success) {
            getCoinsMessage.textContent = result.message ?? "Coins successfully added to balance !";
            getCoinsMessage.hidden = false;
            setTimeout(() => showSection('finishSection'), 2000);
        }
    } catch (error) {
        getCoinsMessage.textContent = "An error occurred. Please try again.";
        getCoinsMessage.hidden = false;
        getCoinsBtn.disabled = false;

        console.error(error);
    }
})