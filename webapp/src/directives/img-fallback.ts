const vImgFallback = {
  created(el: HTMLElement, binding: never) {
    const { value } = binding;
    const img = el as HTMLImageElement;

    img.onerror = () => {
      img.src = value;
    };
  },
};

export default vImgFallback;
