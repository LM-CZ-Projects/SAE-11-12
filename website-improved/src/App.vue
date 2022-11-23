<template>
  <RouterView />
</template>

<style lang="scss">
@use "./assets/css/theme";
@forward "./assets/css/normalize";

html {
  scroll-behavior: smooth;
}

#app {
  width: 100vw;
  height: 100vh;
  overflow: hidden auto;
  color: theme.color("text");
  scroll-snap-type: y mandatory;
  background-color: theme.color("background");
}
</style>
