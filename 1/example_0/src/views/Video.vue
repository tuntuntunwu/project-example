<script setup>
import { store } from '../stores/store.js'

function danmu() {
    let text = document.querySelector(".input-field input").value;
    let div = document.createElement("div");
    div.textContent = text;
    div.style.color = "rgba(255, 255, 255, 0.5)";
    div.style.position = "absolute";
    div.style.top = (Math.random() * 100 / 2) + '%';
    div.style.left = window.getComputedStyle(document.querySelector("#video")).width;
    document.querySelector("#video").after(div);
    animate(div);
}
function animate(e) {
    let times = Math.random() * 10 * 100;

    let selfWidth = parseFloat(window.getComputedStyle(e).width);
    let videoWidth = parseFloat(window.getComputedStyle(document.querySelector("#video")).width);
    let distance = selfWidth + videoWidth;

    let timeDistance = distance / times;

    console.log(timeDistance);

    let timer = setInterval(() => {
        let nowPosition = parseFloat(e.style.left) - timeDistance;
        if (nowPosition < -selfWidth) {
            clearInterval(timer);
            e.remove();
        }

        e.style.left = nowPosition + 'px';
    }, 10)
}
</script>

<template>
    <div class="occupy"></div>
    <div class="main">
        <div class="left-container">
            <div class="title">{{ store.title }}</div>
            <div class="cut card-shadow">
                <div class="video-wrapper">
                    <video id="video" width="668" controls="controls" src="http://vjs.zencdn.net/v/oceans.mp4"></video>
                </div>
                <div class="danmu-input"><div class="input-hide"></div><div class="input-field"><input type="text"></div><div class="input-button" @click="danmu">发送</div></div>
            </div>
        </div>
        <div class="right-container"></div>
    </div>
</template>

<style scoped>
.occupy {
    width: 100%;
    height: 64px;
}
.main {
    margin: auto;
    width: 1320px;
    display: flex;
    justify-content: center;
}

.main .left-container {
    width: 668px;
}
.main .right-container {
    width: 350px;
    height: 3000px;
}

.main .title {
    padding-top: 24px;
    height: 106px;
    font-weight: 500;
    font-size: 20px;
}

.main .cut {
    overflow: hidden;
}
.main .video-wrapper {
    width: 668px;
    height: 376px;
    background-color: black;
    display: flex;
    position: relative;
}

.main .danmu-input {
    height: 46px;
    display: flex;
    padding: 0 12px;
    align-items: center;
}
.main .danmu-input div {
    flex: 0 0 auto;
}
.main .danmu-input .input-hide {
    flex: 1 0 auto;
}

.main .danmu-input .input-field {
    height: 32px;
    width: 230px;
    background-color: #f1f2f3;
    border-radius: 8px 0 0 8px;

    display: flex;
    justify-content: center;
    align-items: center;
}
.main .danmu-input .input-field input {
    border: none;
    outline: none;
    height: calc(100% - 8px);
    background-color: #f1f2f3;
}
.main .danmu-input .input-field:focus {
    border: none;
    outline: none;
}

.main .danmu-input .input-button {
    display: flex;
    justify-content: center;
    align-items: center;

    height: 32px;
    width: 62px;
    background-color: #00aeec;
    border-radius: 0 8px 8px 0;
    font-size: 13px;
    color: white;
}
</style>

