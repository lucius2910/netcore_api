export type ColConfig = {
  key: string;
  key_field: string;
  text_field: string;
  title: string;
  sort?: string | null;
  is_sort?: boolean;
  type?: string;
  number_fixed: number;
  class: string;
};

export type TableConfig = {
  checkbox?: boolean;
  action?: boolean;
  showPaging?: boolean;
  dbClick?: boolean;
  index?: boolean;
};
