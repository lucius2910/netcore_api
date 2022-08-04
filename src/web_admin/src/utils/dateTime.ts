export default {
  getCurrentDate() {
    return new Date().toISOString().slice(0, 10);
  },
  //convert date data form string
  formatDate(input: string) {
    return input ? new Date(input).toISOString().slice(0, 10) : null;
  },
  formatDateTime(input: string) {
    return input
      ? new Date(input).toISOString().replace(/T/, " ").replace(/\..+/, "")
      : null;
  },
};
