<script setup>
import { onMounted } from 'vue';
import { RouterLink } from 'vue-router'

onMounted(() => {
  document.querySelectorAll(".video-card a").forEach((e) => {
    e.addEventListener("click", () => { console.log("hehe") });
  });

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
            <li><RouterLink to="/video"><img src="../assets/focus0.png" alt="" height="373"></RouterLink></li>
            <li><RouterLink to="/video"><img src="../assets/focus1.jpg" alt="" height="373"></RouterLink></li>
            <li><RouterLink to="/video"><img src="../assets/focus2.png" alt="" height="373"></RouterLink></li>
            <li><RouterLink to="/video"><img src="../assets/focus3.jpg" alt="" height="373"></RouterLink></li>
        </ul>
        <!-- 小圆圈 -->
        <ol class="circle"></ol>
      </div>

    <div class="video-card">
      <RouterLink to="/video"><img src="../assets/pic0.jpg" alt="" width="248"></RouterLink>
      <div class="video-title"><b>油条尼亚挑战球形布偶</b></div>
      <div class="up">汤圆爹地</div>
    </div>
    <div class="video-card">
      <RouterLink to="/video"><img src="../assets/pic1.jpg" alt="" width="248"></RouterLink>
      <div class="video-title"><b>完了，开学第一次批作业，把全班作业都批错了......</b></div>
      <div class="up">车棍儿老师在教书</div>
    </div>
    <div class="video-card">
      <RouterLink to="/video"><img src="../assets/pic2.jpg" alt="" width="248"></RouterLink>
      <div class="video-title"><b>我宣布：中单剑圣！回归！！</b></div>
      <div class="up">王者剑圣甲壳虫 . 6-15</div>
    </div>
    <div class="video-card">
      <RouterLink to="/video"><img src="../assets/pic3.jpg" alt="" width="248"></RouterLink>
      <div class="video-title"><b>料敌于先：足球经理（FM）真实临场流的有趣玩法</b></div>
      <div class="up">7BBB的足球战术笔记 . 7-2</div>
    </div>
    <div class="video-card">
      <RouterLink to="/video"><img src="../assets/pic4.jpg" alt="" width="248"></RouterLink>
      <div class="video-title"><b>别瞎练了！盘点那些骗流量的变美谎言</b></div>
      <div class="up">整形医生肖一丁 . 6-16</div>
    </div>
    <div class="video-card">
      <RouterLink to="/video"><img src="../assets/pic5.jpg" alt="" width="248"></RouterLink>
      <div class="video-title"><b>每天剪辑一小时，在家自由接单养活自己</b></div>
      <div class="up">中职通</div>
    </div>
    <div class="video-card">
      <RouterLink to="/video"><img src="../assets/pic6.jpg" alt="" width="248"></RouterLink>
      <div class="video-title"><b>34岁时明白的10个人生真相，希望你24岁就知道 | 社会的毒打</b></div>
      <div class="up">全嘻嘻</div>
    </div>
    <div class="video-card">
      <RouterLink to="/video"><img src="../assets/pic7.jpg" alt="" width="248"></RouterLink>
      <div class="video-title"><b>我要被这群记者笑死啦哈哈哈哈哈哈哈哈哈哈</b></div>
      <div class="up">狗友唠嗑</div>
    </div>
    <div class="video-card">
      <RouterLink to="/video"><img src="../assets/pic8.jpg" alt="" width="248"></RouterLink>
      <div class="video-title"><b>收手吧彦祖，这已经不是猫耳朵套圈的问题了...</b></div>
      <div class="up">橘域网 . 7-4</div>
    </div>
    <div class="video-card">
      <RouterLink to="/video"><img src="../assets/pic9.jpg" alt="" width="248"></RouterLink>
      <div class="video-title"><b>这牛头一万个心眼，太细了。学会了上单牛 王者上青铜</b></div>
      <div class="up">神牛教、教主 . 7-4</div>
    </div>
    <div class="video-card">
      <RouterLink to="/video"><img src="../assets/pic10.jpg" alt="" width="248"></RouterLink>
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
        
.circle {
    list-style-type: none;
    position: absolute;
    bottom: 10px;
    left: 50px;
}
.circle li {
    float: left;
    width: 8px;
    height: 8px;
    border: 2px solid rgba(255, 255, 255, 0.5);
    margin: 0 3px;
    border-radius: 50%;
    cursor: pointer;
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

