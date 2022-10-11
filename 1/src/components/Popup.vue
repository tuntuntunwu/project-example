<script setup>
const props = defineProps(['data']);
</script>

<template>
    <div class="popup card-shadow">
        <ul>
            <li v-for="(d, index) in props.data" :key="index"><a href="">{{ d }}</a></li>
        </ul>
    </div>
</template>

<style>
/* popup */
.custom-navbar .popup {
  background-color: white;
  padding: 8px;
  position: absolute;
  top: 100%;
  left: 50%;
  transform: translate(-50%, 0);
  border-radius: 8px;
  
  display: none;
  /* transition: .2s ease-out .2s; */
}
.custom-navbar > ul > li:hover .popup {
  display: block;
  animation: gradient .2s ease-out;
}
@keyframes gradient {
  0% { opacity: 0; }
  100% { opacity: 1; }
}

/* popup content */
.popup > ul {
  width: max-content;
  list-style: none;
}

.popup > ul > li > a {
  font-size: 11pt;
  color: black;
  text-decoration: none;
  display: block;
  padding: 8px;

  border-bottom: 2px solid transparent;
  position: relative;
}

/* :hover */
.popup > ul > li > a::before {
  content: "";
  top: calc(100% - 4px);
  width: calc(100% - 16px);
  height: 2px;
  background-color: var(--theme-color);

  position: absolute;
  transform: scaleX(0);
  transition: .16s ease-out .1s;
}
.popup > ul > li > a:hover::before {
  transform: scaleX(1);
}
</style>