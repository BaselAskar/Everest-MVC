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

    
    stratSectionSpinner(secElement) {
        const html = `
        <div class="spinner-container">
          <div class="spinner-background"></div>
          <div class="spinner-center">
            <div class="wave"></div>
            <div class="wave"></div>
            <div class="wave"></div>
            <div class="wave"></div>
            <div class="wave"></div>
            <div class="wave"></div>
            <div class="wave"></div>
            <div class="wave"></div>
            <div class="wave"></div>
            <div class="wave"></div>
          </div>
        </div>
        `
        
        secElement.insertAdjacentHTML("afterbegin", html);
    }


    closeSectionSpinner(secElement) {
        secElement.querySelector(".spinner-container").remove();
    }
}

export default new GlobalServices();
