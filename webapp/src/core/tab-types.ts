export class TabItem {
  constructor(itemKey: string, label?: string) {
    this.key = itemKey;
    this.label = label ?? itemKey;
  }

  key: string;
  label: string;
}
