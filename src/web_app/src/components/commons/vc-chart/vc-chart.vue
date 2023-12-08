<template>
	<div class="card z-index-2 ">
		<div class="card-header p-0 position-relative mt-n4 mx-3 z-index-2 bg-transparent">
			<div class="bg-gradient-success shadow-success border-radius-lg py-3 pe-1">
				<div class="chart">
					<canvas :id="id"></canvas>
				</div>
			</div>
		</div>
	</div>
</template>
  
<script setup lang="ts">
import Chart, { type ChartConfiguration, type ChartType, type ChartItem } from 'chart.js/auto';
import { onMounted, toRef } from 'vue';


const props = defineProps<{
	stacked: boolean,
	type: string,
	label: [],
	dataset: [],
	id: string,
}>()

const type = toRef(props, 'type');
const stacked = toRef(props, 'stacked');
const label = toRef(props, 'label');
const dataset = toRef(props, 'dataset');
const _id = Math.floor(Math.random() * 100);

const grid = {
	drawBorder: true,
	display: true,
	drawOnChartArea: false,
	drawTicks: true,
	color: 'rgba(0, 0, 0, 1)'
};

const data = {
	labels: label.value,
	datasets: dataset.value,
};
const config: ChartConfiguration = {
	type: type.value as ChartType,
	data: data,
	options: {
		responsive: true,
		maintainAspectRatio: true,
		plugins: {
			legend: {
				display: true,
			}
		},
		interaction: {
			intersect: true,
			mode: 'index',
		},
		scales: {
			y: {
				stacked: stacked.value,
				beginAtZero: true,
				grace: '5%',
				grid: type.value == "doughnut" ? { drawOnChartArea: false, drawBorder: false, } : grid,
				ticks: {
					display: type.value == "doughnut" ? false : true,
					color: 'rgba(0, 0, 0, 1)',
					padding: 10,
					font: {
						size: 14,
						weight: "300",
						family: "Roboto",
						style: 'normal',
						lineHeight: 2
					},
				},
			},
			x: {
				stacked: stacked.value,
				grid: type.value == "doughnut" ? { drawOnChartArea: false, drawBorder: false, } : grid,
				ticks: {
					display: type.value == "doughnut" ? false : true,
					color: '#rgba(0, 0, 0, 1)',
					padding: 10,
					font: {
						size: 14,
						weight: "300",
						family: "Roboto",
						style: 'normal',
						lineHeight: 2
					},
				}
			},
		},
	},
};

onMounted(() => {
	const canvasTag = document.getElementById(_id.toString()) as ChartItem
	new Chart(
		canvasTag,
		config
	);
});
</script>
  
<style></style>