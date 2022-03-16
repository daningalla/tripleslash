const defaultTheme = "dark";

export const setTheme = (id: string) => {
  document.body.setAttribute("data-theme", id);
  window.localStorage.setItem("theme", id);
}

export function getTheme(): string {
  return document.body.getAttribute("data-theme") ?? defaultTheme;
}

export function initializeTheme(): void {
  setTheme(getTheme());
}
