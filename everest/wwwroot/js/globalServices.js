class GlobalServices {
  startButtonSpinner(btnElement) {
    btnElement.innerHTML = `
      <span class="spinner-border spinner-border-sm" data-content="${btnElement.textContent}"></span>
    `;
  }

  stopButtonSpinner(btnElement) {
    const textContent = btnElement.querySelector("span").dataset.content;

    btnElement.innerHTML = textContent;
  }
}

export default new GlobalServices();
