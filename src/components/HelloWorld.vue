<template>
  <div class="hello">
    <!-- <span>{{riverData.value.timeSeries[0].sourceInfo.siteName}}</span>
    <ul>
      <li :v-for="gaugeData in riverData.value.timeSeries">
        <h2>{{ gaugeData.variable.variableName }}</h2>
        <h4>{{ gaugeData.variable.variableDescription }}</h4>
        <h4>Unit: {{gaugeData.variable.unit.unitCode}}</h4>
      </li>
    </ul> -->
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import {ServerResponse} from '../data/ServerResponse';
import axios from 'axios';
import { USGSData } from '../data/usgsData';

@Component
export default class HelloWorld extends Vue {
  @Prop() private msg!: string;
  name = "RiverData";
  data(){
    return{
      riverData: {} as USGSData
    }    
  }
  mounted(){
    axios.request<USGSData>({
      url: 'https://waterservices.usgs.gov/nwis/iv/?format=json&sites=04201500&period=PT4H&siteStatus=all',
      transformResponse: (r: ServerResponse) => r.data
    }).then((response) => {
      console.log(response.data);
      this.$data.riverData = response.data;
    })
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
