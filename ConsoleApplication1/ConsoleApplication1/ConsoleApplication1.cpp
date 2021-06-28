﻿#include<stdio.h>
#include<stdlib.h>
#include<time.h>
//----------------------------------------------77-08
// Распределяющий подсчет - сортировка за один цикл
void sort(int A[], int n) {
	int i, j, max;
	for (i = 0, max = 0; i < n; i++) if (A[i] > max) max = A[i];
	int* cnt = new int[max + 2];			// массив счетчиков по всему
	int* out = new int[n];				// значений
	for (i = 0; i <= max + 1; i++) cnt[i] = 0;
	// вариант 1 -------------------------------------------------------
	//for (i=0; i<n; i++) cnt[A[i]]++;	// накопление счетчиков значений
	//for (i=0,j=0; i<=max; i++)	
	//	while(cnt[i]--!=0) out[j++]=i;	// копирование каждого значения
	// вариант 2 -------------------------------------------------------
	for (i = 0; i < n; i++) cnt[A[i] + 1]++;	// накопление счетчиков значений
	for (i = 1; i <= max; i++)				// добавление к каждому счетчику
		cnt[i] += cnt[i - 1];				// суммы предыдущих
	for (i = 0; i < n; i++)					// перенос в выходную позицию
		out[cnt[A[i]]++] = A[i];			// в соответствии со счетчиком предыдущих
	//------------------------------------------------------------------
	for (i = 0; i < n; i++) A[i] = out[i];
	delete[] cnt;
	delete[] out;
}


void main()
{
	int i, n = 200, * A = new int[n];
	srand(time(NULL));
	for (i = 0; i < n; i++) A[i] = rand() % 10 + 1;
	sort(A, n);
	puts("");
	for (i = 0; i < n; i++) printf("%d ", A[i]);
}