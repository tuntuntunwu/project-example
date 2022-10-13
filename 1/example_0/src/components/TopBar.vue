<script setup>
import { computed } from 'vue'
import { useStore } from 'vuex'
import Popup from './Popup.vue';
import { RouterLink } from 'vue-router'

const paihangData = ["综合热门", "每周必看", "入站必刷", "排行榜"];
const xiaoxiData = ["回复我的" , "@我的", "收到的赞", "我的消息", "系统通知"];

const vuexStore = useStore();
const count = computed(() => vuexStore.state.list);
</script>

<template>
    <div class="custom-navbar card-shadow">
      <ul>
        <li><RouterLink class="main-content" to="/"><img src="../assets/BILIBILI_LOGO.svg" width="52.5" /></RouterLink></li>
        <li><a class="main-content" href="">主站</a></li>

        <li>
          <a class="main-content" href="">排行</a>
          <Popup :data="paihangData" />
        </li>

        <li><a class="main-content" href="">番剧</a></li>
        <li><a class="main-content" href="">直播</a></li>
        <li><a class="main-content" href="">赛事</a></li>
        <li><a class="main-content" href="">会员购</a></li>
        <li><a class="main-content" href="">漫画</a></li>
        <li class="disabled"></li>

        <li>
          <div class="main-content">
            <form class="" action="">
              <input type="text" placeholder="中国人的钱就这么好赚？" list="datalist" />
              <datalist id="datalist">
                <option v-for="item in count" :value="item" />
              </datalist>
            </form>
          </div>
        </li>

        <li class="disabled"></li>

        <li><a class="main-content" href="">profile</a></li>

        <li>
          <a class="main-content" href="">消息</a>
          <Popup :data="xiaoxiData" />
        </li>

        <li><a class="main-content" href="">动态</a></li>
        <li><a class="main-content" href="">订阅</a></li>
        <li><a class="main-content" href="">稍后再看</a></li>
        <li><a class="main-content" href="">收藏</a></li>
        <li><a class="main-content" href="">历史</a></li>
        <li><a class="main-content" href="">投稿</a></li>
        <li><a class="main-content" href="">swith</a></li>
      </ul>
    </div>
</template>

<style>
/* navbar */
.custom-navbar {
  position: absolute;
  top: 0;
  left: 0;
  height: 50px;
  width: 100%;
  background-color: white;
  color: black;
  
  z-index: 10001;
  display: flex;
  justify-content: center;

}

/* navbar content */
.custom-navbar > ul {
  margin: 0 10%;
  flex-grow: 1;
  list-style: none;
  height: 100%;

  display: flex;
}

.custom-navbar > ul > li {
  flex: 0 0 auto;
  cursor: pointer;
  position: relative;
  height: 100%;
  display: flex;
  align-items: center;

  transition: .2s ease-out;
}
.custom-navbar > ul > li.disabled {
  flex: 1 0 auto;
  cursor: auto;
}

.custom-navbar .main-content {
  font-size: 10pt;
  color: black;
  text-decoration: none;
  display: block;
  padding: 0 10px;
}
.custom-navbar .main-content input {
  height: 32px;
  border: none;
  outline: none;
}

/* :hover */
.custom-navbar > ul > li:hover:not(.disabled) {
  background-color: rgba(0, 0, 0, 0.1);
}

/* icon */
.custom-navbar-icon-logo::before {
  content: "";
}
</style>