import AccountAPI from "../API/AccountAPI";
import Vuex from "vuex";
import Vue from "vue";

Vue.use(Vuex);

const store = new Vuex.Store({
  state: {
    APP_CONTEXT: {
      user: null,
      company: null
    },
  },
  getters: {
    getContext(state) {
      return state.APP_CONTEXT;
    }
  },
  mutations: {
    setAppContext(state, payload) {
      state.APP_CONTEXT.user = payload.user;
      state.APP_CONTEXT.company = payload.company;
    },
  }
});

export default {
  setup() {
    return new Promise((resolve, reject) => {
      AccountAPI.getApplicationContext().then(appContext => {
          store.commit("setAppContext", appContext.data);
          resolve();
        })
        .catch(() => reject());
    });
  },
  getContext() {
    return store.getters.getContext;
  },
};