<template>
  <div class="river">
    <template v-if="!isLoaded">
      Loading...    
    </template>
    <template v-else>
      <span>{{riverData.value.timeSeries[0].sourceInfo.siteName}}</span>
      <ul>
        <li v-for="gaugeData in riverData.value.timeSeries" :key="gaugeData.name">
          <h4 v-html="gaugeData.variable.variableName">{{ gaugeData.variable.variableName }}</h4>
          <h5>{{ gaugeData.variable.variableDescription }}</h5>
          <hr>
          <h3>{{gaugeData.values[0].calculatedValue}}</h3>
        </li>
      </ul>
    </template>
    
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import {ServerResponse} from '../data/ServerResponse';
import { getById } from '../services/apiClient';
import { USGSData } from '../data/usgsData';

@Component
export default class River extends Vue {
  public name = 'RiverData';
  public riverData!: USGSData;
  public isLoaded: boolean = false;

  @Prop() private riverId!: string;

  public async mounted() {
    await this.GetRiverData();
    this.isLoaded = true;
  }

  private async GetRiverData() {
    this.riverData = await getById(this.riverId);
    this.riverData.value.timeSeries.forEach((element) => {
      const values = element.values[0].value.sort((a, b) => {
        return +new Date(b.dateTime) - +new Date(a.dateTime);
      });
      if(values[0] === undefined) return;
      const value = element.values[0];
      element.variable.variableName = element.variable.variableName.split(',')[0];

      if (values[0].value > values[1].value) {
        value.calculatedValue = values[0].value + '↑';
      } else if (values[0].value === values[1].value) {
        value.calculatedValue = values[0].value + '-';
      } else {
        value.calculatedValue = values[0].value + '↓';
      }
    });
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.river {
  display: inline-block;
  max-width: 250px;
  max-height: 250px;
  border: 1px solid;
  background-color: #5c5c5c;
}
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
