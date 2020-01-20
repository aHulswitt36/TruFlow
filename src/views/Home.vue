<template>
  <div class="home">
    <River riverId='04201500'/>
    <River riverId='04208000'/>
    <River riverId='04209000'/>
  </div>
</template>

<script>
// @ is an alias to /src
import River from '@/components/River.vue';

export default {
  name: 'home',
  components: {
    River,
  },
};
</script>
