<script setup>
import { useStore } from 'vuex';
import { RouterLink } from 'vue-router'
import { store } from '../stores/store.js'
import { onMounted } from 'vue';

const vuexStore = useStore();
function changeStore(e) {
  let title = e.currentTarget.nextElementSibling.firstElementChild.textContent;
  store.title = title;
  vuexStore.commit('append', title);
}

onMounted(() => {

let focus = document.querySelector('.focus');
let arrowl = document.querySelector('.arrow-l');
let arrowr = document.querySelector('.arrow-r');

// 载入后自动开始轮播
let timer = setInterval(() => { arrowr.click(); }, 2000);
// 鼠标移入停止轮播
focus.addEventListener('mouseenter', function() {
    // 停止轮播
    clearInterval(timer);
    timer = null;
});
focus.addEventListener('mouseleave', function() {
    // 开始轮播
    timer = setInterval(() => { arrowr.click(); }, 2000);
});

// 克隆第一张图片(li)放到ul最后面作为辅助图，如没有辅助图衔接，滑动会出现空白区域
// 在此处代码生成好处理
let ul = focus.querySelector('ul');
let first = ul.children[0].cloneNode(true);
ul.append(first);

let num = 0;  // 切换的索引号
let showWidth = focus.offsetWidth;  // 轮播偏移量
// 按钮操作
let flag = true;  // 节流阀开关
arrowr.addEventListener('click', function() {
    if (flag) {
        // 关节流阀
        flag = false;

        // 修改图片
        // 辅助图在这里起效
        if (num == 4) {
            ul.style.left = 0;
            num = 0;
        }
        num++;

        animate(ul, -num * showWidth);

        // 开节流阀
        flag = true;
    }
});
arrowl.addEventListener('click', function() {
    if (flag) {
        flag = false;

        // 修改图片
        if (num == 0) {
            num = 4;
            ul.style.left = -num * showWidth + 'px';
        }
        num--;

        animate(ul, -num * showWidth);
        
        flag = true;
    }
});

function animate(elt, targetLeft) {
    // 执行非常多次小段位移，从而模拟滑动效果
    // targetLeft: 0 -516 -1032 -1548 -2064

    clearInterval(elt.timer);  // 在执行一段animate时，可能发生多次节流操作。即在执行一个animate异步操作时，会有新animate执行，因而需要先clearInterval
    elt.timer = setInterval(function() {
        // 每15毫秒进行1次位移，位移量为 (目标值 - 现在的位置) / 10，此位移量慢慢变小以模拟滑动时的渐进效果
        let step = (targetLeft - elt.offsetLeft) / 10;
        step = step > 0 ? Math.ceil(step) : Math.floor(step);  // 整数值：1、效果好。2、方便设置停止条件，===即可。
        elt.style.left = elt.offsetLeft + step + 'px';

        // 到达目标位置，停止滑动
        if (elt.offsetLeft <= targetLeft) {
            clearInterval(elt.timer);
        }
    }, 15);
}

});



</script>

<template>
<div class="banner">
    <div class="banner-inner">
      <img src="../assets/banner.jpg" alt="banner" />
    </div>
  </div>

  <div class="main">
      <div class="focus">
        <!-- 左侧按钮 -->
        <a class="arrow-l">&lt;</a>
        <!-- 右侧按钮 -->
        <a class="arrow-r">&gt;</a>
        <!-- 核心的滚动区域 -->
        <ul>
            <li><RouterLink to="/video" @click="store.title = '找工作'"><img src="../assets/focus0.jpg" alt="" width="516" height="373"></RouterLink></li>
            <li><RouterLink to="/video" @click="store.title = '入殓师'"><img src="../assets/focus1.jpg" alt="" width="516" height="373"></RouterLink></li>
            <li><RouterLink to="/video" @click="store.title = '减肥'"><img src="../assets/focus2.jpg" alt="" width="516" height="373"></RouterLink></li>
            <li><RouterLink to="/video" @click="store.title = '原神'"><img src="../assets/focus3.jpg" alt="" width="516" height="373"></RouterLink></li>
        </ul>
      </div>

    <div class="video-card">
      <RouterLink to="/video" @click="changeStore"><img src="../assets/pic0.jpg" alt="" width="248"></RouterLink>
      <div class="video-title"><b>油条尼亚挑战球形布偶</b></div>
      <div class="up">汤圆爹地</div>
    </div>
    <div class="video-card">
      <RouterLink to="/video" @click="changeStore"><img src="../assets/pic1.jpg" alt="" width="248"></RouterLink>
      <div class="video-title"><b>完了，开学第一次批作业，把全班作业都批错了......</b></div>
      <div class="up">车棍儿老师在教书</div>
    </div>
    <div class="video-card">
      <RouterLink to="/video" @click="changeStore"><img src="../assets/pic2.jpg" alt="" width="248"></RouterLink>
      <div class="video-title"><b>我宣布：中单剑圣！回归！！</b></div>
      <div class="up">王者剑圣甲壳虫 . 6-15</div>
    </div>
    <div class="video-card">
      <RouterLink to="/video" @click="changeStore"><img src="../assets/pic3.jpg" alt="" width="248"></RouterLink>
      <div class="video-title"><b>料敌于先：足球经理（FM）真实临场流的有趣玩法</b></div>
      <div class="up">7BBB的足球战术笔记 . 7-2</div>
    </div>
    <div class="video-card">
      <RouterLink to="/video" @click="changeStore"><img src="../assets/pic4.jpg" alt="" width="248"></RouterLink>
      <div class="video-title"><b>别瞎练了！盘点那些骗流量的变美谎言</b></div>
      <div class="up">整形医生肖一丁 . 6-16</div>
    </div>
    <div class="video-card">
      <RouterLink to="/video" @click="changeStore"><img src="../assets/pic5.jpg" alt="" width="248"></RouterLink>
      <div class="video-title"><b>每天剪辑一小时，在家自由接单养活自己</b></div>
      <div class="up">中职通</div>
    </div>
    <div class="video-card">
      <RouterLink to="/video" @click="changeStore"><img src="../assets/pic6.jpg" alt="" width="248"></RouterLink>
      <div class="video-title"><b>34岁时明白的10个人生真相，希望你24岁就知道 | 社会的毒打</b></div>
      <div class="up">全嘻嘻</div>
    </div>
    <div class="video-card">
      <RouterLink to="/video" @click="changeStore"><img src="../assets/pic7.jpg" alt="" width="248"></RouterLink>
      <div class="video-title"><b>我要被这群记者笑死啦哈哈哈哈哈哈哈哈哈哈</b></div>
      <div class="up">狗友唠嗑</div>
    </div>
    <div class="video-card">
      <RouterLink to="/video" @click="changeStore"><img src="../assets/pic8.jpg" alt="" width="248"></RouterLink>
      <div class="video-title"><b>收手吧彦祖，这已经不是猫耳朵套圈的问题了...</b></div>
      <div class="up">橘域网 . 7-4</div>
    </div>
    <div class="video-card">
      <RouterLink to="/video" @click="changeStore"><img src="../assets/pic9.jpg" alt="" width="248"></RouterLink>
      <div class="video-title"><b>这牛头一万个心眼，太细了。学会了上单牛 王者上青铜</b></div>
      <div class="up">神牛教、教主 . 7-4</div>
    </div>
    <div class="video-card">
      <RouterLink to="/video" @click="changeStore"><img src="../assets/pic10.jpg" alt="" width="248"></RouterLink>
      <div class="video-title"><b>12种欲望原型人格，测测你是哪一种</b></div>
      <div class="up">趣测酱 . 7-4</div>
    </div>
  </div>

  <div>

  </div>
</template>

<style scoped>
.banner {
  width: 100%;
  height: 155px;
  overflow: hidden;
}
.banner-inner {
  width: 100%;
  height: 155px;
  overflow: scroll;
}

.main {
  margin: 30px auto;
  width: 1320px;
  display: grid;
  grid-template-columns: repeat(5, 1fr);
  grid-gap: 20px;
}

/* carousel */
.focus {
  grid-row: 1 / 3;
  grid-column: 1 / 3;
  width: 516px;
  height: 373px;  /* imgs are 516✖️373 */
  background-color: white;  /* can be used when debugged */
  overflow: hidden;
  border-radius: 8px;

    position: relative;
}

.focus ul {
    list-style-type: none;
    position: absolute;
    top: 0;
    left: 0;
    width: 500%;  /* 4 imgs and 1 slot to implement img alternation */
}
.focus ul li {
    float: left;
}
        
.arrow-l,
.arrow-r {
    display: block;
    position: absolute;
    z-index: 1;  /* over ul */
    bottom: 48px;
    right: 56px;
    width: 28px;
    height: 28px;

    line-height: 28px;
    text-align: center;
    background: rgba(255, 255, 255, .2);
    color: #FFFFFF;
    font-size: 12px;
    cursor: pointer;
    border-radius: 8px;
}
.arrow-r {
    right: 16px;
}
.arrow-l:hover,
.arrow-r:hover {
  background: rgba(255, 255, 255, .4);
}

.current {
    background-color: #FFFFFF;
}

/* video-card */
.video-card {
  width: 248px;
  height: 214px;
  background-color: white;
}

.video-card img {
  display: block;
  border-radius: 8px;
}

.video-card .video-title {
  margin-top: 10px;
  height: 44px;
}

.video-card .up {
  margin-top: 4px;
  height: 16px;
  line-height: 16px;
  font-size: 13px;
  color: #9499A0;;
}
</style>

