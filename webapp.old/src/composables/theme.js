/**
 * Applies a theme and save the setting to local storage.
 * @param {String} id The id of the theme to set.
 */
export const setTheme = (id) => {
    document.body.setAttribute('data-theme', id);
    window.localStorage.setItem('theme', id);
}

/**
 * Initializes themeing.
 */
export const useTheme = () => {
    const id = window.localStorage.getItem('theme') ?? 'dark';
    setTheme(id);
};