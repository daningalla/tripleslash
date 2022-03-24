const svgSource = `
 <svg
    id="Layer_1"
    data-name="Layer 1"
    xmlns="http://www.w3.org/2000/svg"
    viewBox="0 0 64 64"
  >
    <style>
      .st0 { fill: #888888; }
    </style>
    <circle class="st0" cx="6.5" cy="6.5" r="6.5" />
    <path
      class="st0"
      d="M50.42,15H28.58A13.58,13.58,0,0,0,15,28.58V50.42A13.58,13.58,0,0,0,28.58,64H50.42A13.58,13.58,0,0,0,64,50.42V28.58A13.58,13.58,0,0,0,50.42,15ZM26.5,33A6.5,6.5,0,1,1,33,26.5,6.5,6.5,0,0,1,26.5,33Zm22,26A10.5,10.5,0,1,1,59,48.5,10.5,10.5,0,0,1,48.5,59Z"
    />
  </svg>
`;

const blob = new Blob([svgSource], { type: "image/svg+xml" });
const sourceUrl = URL.createObjectURL(blob);

export default sourceUrl;
