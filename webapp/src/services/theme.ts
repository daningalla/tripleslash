export interface Theme {
  key: string;
  displayName: string;
}

export class ThemeService {
  /**
   * Gets the available themes.
   */
  themes: Theme[] = [
    { key: "dark", displayName: "Dark (default)" },
    { key: "light", displayName: "Light" },
  ];

  /**
   * Sets the current theme
   * @param key theme key
   */
  setTheme(key: string): void {
    const theme = this.themes.find((t) => t.key === key);
    if (!theme){
      throw new Error(`Undefined theme '${key}'`);
    }
    window.localStorage.setItem("theme", key);
    document.body.setAttribute("data-theme", key);
  }

  /**
   * Initializes the current theme.
   */
  initialize(): void {
    this.setTheme(window.localStorage.getItem("theme") ?? "dark");
  }
}

export const themeService = new ThemeService();
