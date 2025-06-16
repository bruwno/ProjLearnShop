/** @type {import('tailwindcss').Config} */
const colors = require('tailwindcss/colors');


module.exports = {
    content: [
        './**/*.razor',
        './**/*.html',
        './**/*.cshtml',
        './wwwroot/**/*.html'
    ],
    theme: {
        extend: {
            colors: {
                ...colors,
                primary: '#151773',
                secondary: '#4f52d9',
                highlight: "#76be11",
                gray: "#b3b3b9"
            }
        },
    },
    plugins: [],
}
