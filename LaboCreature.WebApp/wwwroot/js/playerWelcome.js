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

const chooseCreatureForm = document.getElementById("chooseCreatureForm");
const chooseCreatureBtn = document.getElementById("chooseCreatureButton");
const chooseCreatureMessage = document.getElementById("chooseCreatureMessage");

const raceSelection = document.getElementById("raceSelection");
const nicknameInput = document.getElementById("nickname");

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
            setTimeout(async () => {
                showSection('creatureSection');
                await loadRandomRaces();
            }, 2000);
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

async function loadRandomRaces() {
    try {
        const response = await fetch('/Player/GetRandomRaces');

        if (!response.ok) {
            throw new Error("Unable to retrieve races.");
        }

        const result = await response.json();

        raceSelection.innerHTML = "";

        result.forEach(race => {
            const card = document.createElement("label");
            card.classList.add("race-card");

            const image = document.createElement("img");
            image.src = race.frontSprite;
            image.alt = race.name;

            const name = document.createElement("span");
            name.textContent = race.name;

            const radio = document.createElement("input");
            radio.type = "radio";
            radio.name = "raceId";
            radio.value = race.id;
            radio.required = true;

            radio.addEventListener("change", () => {
                document.querySelectorAll(".race-card")
                    .forEach(c => c.classList.remove("selected"));

                card.classList.add("selected");

                updateCreatureButton();
            });

            card.append(radio, image, name);
            raceSelection.appendChild(card);
        });

    } catch (error) {
        raceSelection.textContent = "Unable to load races. Please try again.";
        console.error(error);
    }
}

function updateCreatureButton() {
    const selectedRace = chooseCreatureForm.querySelector(
        'input[name="raceId"]:checked'
    );

    chooseCreatureBtn.disabled =
        !selectedRace || !nicknameInput.value.trim();
}

nicknameInput.addEventListener("input", updateCreatureButton);

chooseCreatureForm.addEventListener("submit", async (event) => {
    event.preventDefault();

    chooseCreatureBtn.disabled = true;

    try {
        const response = await fetch(chooseCreatureForm.action, {
            method: 'POST',
            body: new FormData(chooseCreatureForm)
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
            chooseCreatureMessage.textContent =
                result.message ?? "Creature successfully created!";

            chooseCreatureMessage.hidden = false;

            setTimeout(() => showSection('coinsSection'), 2000);
        }
        else {
            chooseCreatureMessage.textContent =
                result.message ?? "Something went wrong. Please try again.";

            chooseCreatureMessage.hidden = false;
            updateCreatureButton();
        }

    } catch (error) {
        chooseCreatureMessage.textContent =
            "An error occurred. Please try again.";

        chooseCreatureMessage.hidden = false;
        updateCreatureButton();

        console.error(error);
    }
});

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