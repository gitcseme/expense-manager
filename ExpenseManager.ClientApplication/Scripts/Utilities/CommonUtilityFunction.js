export default {
  formatMoney(amount) {
    let formatter = new Intl.NumberFormat(undefined, {
      style: "currency",
      currency: "BDT"
      //minimumFractionDigits: 0, // (this suffices for whole numbers, but will print 2500.10 as $2,500.1)
      //maximumFractionDigits: 0, // (causes 2500.99 to be printed as $2,501)
    });

    return formatter.format(amount);
  },
  formattedToNormalMoney(amount) {
    let value = amount.substring(4).replace(",", "").trim();
    return parseFloat(value);
  },
  showNotification(vmReference, message, icon, type) {
    vmReference.$notify({
      message: message,
      icon: icon,
      horizontalAlign: "right",
      verticalAlign: "top",
      type: type
    });
  },
  async prepareCategories(categoryTree, categories) {
    let catIdSet = new Set(categories);
    categories.forEach(catId => {
      categoryTree.forEach(cat => {
        this.collectNodes(catIdSet, cat, catId, cat.id == catId);
      });
    });

    return [...catIdSet];
  },
  collectNodes(catIdSet, cat, catId, isParent) {
    cat.children.forEach(node => {
      if (isParent) {
        catIdSet.add(node.id);
        this.collectNodes(catIdSet, node, catId, true);
      }
      else if (node.id == catId) {
        this.collectNodes(catIdSet, node, catId, true);
      }
      else {
        this.collectNodes(catIdSet, node, catId, false);
      }
    });
  }
};