import type { App } from "vue";

import VcToast from "./alert/vc-toast.vue";
import VcAlert from "./alert/vc-alert.vue";
import VcForm from "./form/vc-form.vue";
import VcButton from "./form/vc-button.vue";
import VcInput from "./form/vc-input.vue";
import VcLabel from "./form/vc-label.vue";
import VcSelect from "./form/vc-select.vue";
import VcInputGroup from "./form/vc-input-group.vue";
import VcCheckbox from "./form/vc-checkbox.vue";
import VcTextarea from "./form/vc-textarea.vue";
import VcInputDate from "./form/vc-input-date.vue";
import VcIcon from "./form/vc-icon.vue";
import VcTree from "./form/vc-tree.vue";
import VcConfirm from "./dialog/vc-confirm.vue";
import VcModal from "./dialog/vc-modal.vue";
import VcCard from "./card/vc-card.vue";
import VcCardTitle from "./card/vc-card-title.vue";
import VcCardContent from "./card/vc-card-content.vue";
import VcCardAction from "./card/vc-card-action.vue";
import VcAccount from "./vc-account/vc-account.vue";
import VcTable from "./vc-table/vc-table.vue";
import VcRow from "./layout/vc-row.vue";
import VcCol from "./layout/vc-col.vue";

const requireComponent = {
  VcForm,
  VcButton,
  VcInput,
  VcToast,
  VcAlert,
  VcConfirm,
  VcCheckbox,
  VcSelect,
  VcInputGroup,
  VcInputDate,
  VcCard,
  VcLabel,
  VcTree,
  VcCardTitle,
  VcCardContent,
  VcCardAction,
  VcTable,
  VcModal,
  VcAccount,
  VcTextarea,
  VcIcon,
  VcRow,
  VcCol,
};

const register = (app: App<Element>): void => {
  Object.entries(requireComponent).forEach(([name, component]) => {
    app.component(name, component);
  });
};

export default {
  register,
};
