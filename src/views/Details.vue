<template>
  <div class="details">
    <River :riverId='$route.params.id'/>
  </div>
</template>

<script>
// @ is an alias to /src
import River from '@/components/River.vue';

export default {
  name: 'details',
  components: {
    River,
  },
};
</script>