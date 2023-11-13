<template>
  <div class="vc-page">
    <!-- <div class="row">
        <vc-chart :dataset="dataset" type="bar" :label="labels" :stacked="true" :grid="barlinegrid"></vc-chart>

        <vc-chart :dataset="lineDataset" type="line" :label="labels" :stacked="false" :grid="barlinegrid"></vc-chart>

        <vc-chart :dataset="doughnutDataset" type="doughnut" :label="doughnutlabels" :stacked="false" :grid="doughnutgrid"></vc-chart>
      </div> -->

    <vc-row :gutter="12">
      <vc-col :lg="8" :md="12" :sm="12" :xs="24">
        <vc-card>
          <vc-chart :dataset="dataset" type="bar" :label="labels" :stacked="true" :grid="barlinegrid"></vc-chart>
        </vc-card>
      </vc-col>

      <vc-col :lg="8" :md="12" :sm="12" :xs="24">
        <vc-card>
          <vc-chart :dataset="lineDataset" type="line" :label="labels" :stacked="false" :grid="barlinegrid"></vc-chart>
        </vc-card>
      </vc-col>

      <vc-col :lg="8" :md="12" :sm="12" :xs="24">
        <vc-card>
          <vc-chart :dataset="doughnutDataset" type="doughnut" :label="doughnutlabels" :stacked="false"
            :grid="doughnutgrid"></vc-chart>
        </vc-card>
      </vc-col>
    </vc-row>
  </div>
</template>

<script setup lang="ts">
const labels = ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat',];

const dataset = [
  {
    label: 'My First dataset',
    backgroundColor: "rgba(58, 39, 245, 0.8)",
    borderColor: 'rgb(255, 99, 132)',
    data: [0, 10, 5, 2, 20, 30, 45],
    fill: true,
    tension: 0.5,
  },
  {
    label: 'My Second dataset',
    backgroundColor: "rgba(255, 38, 38, .8)",
    borderColor: 'rgb(255, 99, 132)',
    data: [1, 10, 5, 2, 20, 30, 45],
    fill: true,
    tension: 0.5,
  },
  {
    label: 'My Third dataset',
    backgroundColor: "rgba(0, 0, 0, 1)",
    borderColor: 'rgb(255, 99, 132)',
    data: [2, 10, 5, 2, 20, 30, 45],
    fill: true,
    tension: 0.5,
  },
];

const lineDataset = [
  {
    label: 'My First dataset',
    backgroundColor: "rgba(0, 0, 0, 0)",
    borderColor: 'rgba(58, 39, 245, 0.8)',
    data: [9, 18, 15, 34, 7, 37, 6],
    fill: true,
    tension: 0.5,
  },
  {
    label: 'My Second dataset',
    backgroundColor: "rgba(0, 0, 0, 0)",
    borderColor: 'rgba(255, 38, 38, .8)',
    data: [41, 16, 38, 31, 5, 23, 43],
    fill: true,
    tension: 0.5,
  },
  {
    label: 'My Third dataset',
    backgroundColor: "rgba(0, 0, 0, 0)",
    borderColor: 'rgba(0, 0, 0, 1)',
    data: [17, 35, 24, 40, 25, 17, 20],
    fill: true,
    tension: 0.5,
  },
];

const doughnutDataset = [
  {
    backgroundColor: ["rgba(213, 2, 2, 1)", "rgba(114, 245, 39, 0.8)", "rgba(0, 0, 0, 0.99)", "rgba(84, 39, 245, 0.8)"],
    fill: true,
    data: [100, 100, 100],
    tension: 0.5,
  },
];

const doughnutlabels = ['My First dataset', 'My Second dataset', 'My Third dataset',]

const barlinegrid = {
  drawBorder: true,
  display: false,
  drawOnChartArea: false,
  drawTicks: true,
  color: 'rgba(0, 0, 0, 1)'
};

const doughnutgrid = {
  drawBorder: false,
  display: false,
  drawOnChartArea: false,
  drawTicks: false,
  color: 'rgba(0, 0, 0, 1)'
}

</script>