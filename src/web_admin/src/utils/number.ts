export default {
  format(input: any, fix: number) {
    return input
      ? Number(Number(input).toFixed(fix ?? 0)).toLocaleString()
      : null;
  },
};
