const form = document.getElementById("signup-form");
const regionList = document.getElementById("region-list");
const statusEl = document.getElementById("status");
const submitBtn = document.getElementById("submit-btn");

let selectedRegion = null;

function setStatus(message, kind) {
  statusEl.textContent = message;
  statusEl.classList.remove("is-ok", "is-error");
  if (kind) {
    statusEl.classList.add(kind);
  }
}

function renderRegions(regions) {
  regionList.innerHTML = "";

  regions.forEach((region, index) => {
    const button = document.createElement("button");
    button.type = "button";
    button.className = "region-option";
    button.dataset.value = String(region.value);
    button.setAttribute("role", "radio");
    button.setAttribute("aria-checked", "false");
    button.innerHTML = `${region.nameTh}<small>${region.nameEn}</small>`;

    button.addEventListener("click", () => {
      selectedRegion = region.value;
      regionList.querySelectorAll(".region-option").forEach((el) => {
        el.setAttribute("aria-checked", el === button ? "true" : "false");
      });
      setStatus("", null);
    });

    if (index === 0) {
      button.click();
    }

    regionList.appendChild(button);
  });
}

async function loadRegions() {
  const response = await fetch("/api/regions");
  if (!response.ok) {
    throw new Error("โหลดรายการภูมิภาคไม่สำเร็จ");
  }

  const regions = await response.json();
  renderRegions(regions);
}

form.addEventListener("submit", async (event) => {
  event.preventDefault();
  setStatus("", null);

  if (selectedRegion == null) {
    setStatus("กรุณาเลือกภูมิภาค", "is-error");
    return;
  }

  const payload = {
    displayName: document.getElementById("displayName").value.trim(),
    email: document.getElementById("email").value.trim(),
    region: selectedRegion
  };

  submitBtn.disabled = true;

  try {
    const response = await fetch("/api/subscribe", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload)
    });

    const result = await response.json();
    setStatus(result.message, result.success ? "is-ok" : "is-error");

    if (result.success) {
      form.reset();
      const first = regionList.querySelector(".region-option");
      if (first) {
        first.click();
      }
    }
  } catch {
    setStatus("เกิดข้อผิดพลาด กรุณาลองใหม่", "is-error");
  } finally {
    submitBtn.disabled = false;
  }
});

loadRegions().catch(() => {
  setStatus("โหลดหน้าไม่สำเร็จ กรุณารีเฟรช", "is-error");
});
