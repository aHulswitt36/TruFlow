<template>
  <div class="river">
    <template v-if="!isLoaded">
      Loading...    
    </template>
    <template v-else>
      <span>{{filteredRiverData[0].sourceInfo.siteName}}</span>
      <ul>
        <li v-for="gaugeData in filteredRiverData" :key="gaugeData.name">
          <h4>{{ gaugeData.variable.unit.unitCode }}</h4>
          <!-- <h5>{{ gaugeData.variable.unit.unitCode }}</h5> -->
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

  get filteredRiverData(){
    if(this.isLoaded){
      return this.riverData.value.timeSeries.filter(function(rd) {
        if(rd.variable.variableDescription.includes("Temperature")){
          return rd;
        }
        if(rd.variable.variableDescription.includes("Discharge")){
          return rd;
        }
        if(rd.variable.variableDescription.includes("Gage")){
          return rd;
        }
        if(rd.variable.variableDescription.includes("Turbidity")){
          return rd;
        }   
      });
    }
    
  }

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
  max-width: 20em;
  max-height: 250px;
  border: 1px solid;
  box-shadow: 2px 2px 10px #5c5c5c;
  padding: 25px;
  margin: 10px;
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
