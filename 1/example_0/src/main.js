import { createApp } from 'vue'
import { createPinia } from 'pinia'
import { createStore } from 'vuex'
import router from './router'

import App from './App.vue'
import './assets/main.css'

const app = createApp(App);

app.use(createPinia());

const vuexStore = createStore({
    state: {
        list: ["中国人的钱就这么好赚"]
    },
    mutations: {
      append (state, payload) {
        if (!state.list.includes(payload)) {
          state.list.push(payload);
        }
      }
    }
  })
app.use(vuexStore);

app.use(router);

app.mount('#app');

