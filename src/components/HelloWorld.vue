<template>
  <div class="hello">
    <span>{{riverData.value.timeSeries[0].sourceInfo.siteName}}</span>
    <ul>
      <li :v-for="gaugeData in riverData.value.timeSeries">
        <h2>{{ gaugeData.variable.variableName }}</h2>
        <h4>{{ gaugeData.variable.variableDescription }}</h4>
        <h4>Unit: {{gaugeData.variable.unit.unitCode}}</h4>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import {ServerResponse} from '../data/ServerResponse';
import { getById } from '../services/apiClient';
import { USGSData } from '../data/usgsData';

@Component
export default class HelloWorld extends Vue {
  @Prop() private msg!: string;
  name = "RiverData"; 
  riverData!: USGSData;

  async created(){
    this.riverData = await getById('04201500');
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
