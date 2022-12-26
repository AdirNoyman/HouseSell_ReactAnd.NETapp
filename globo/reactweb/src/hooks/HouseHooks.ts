// import { useNavigate } from 'react-router-dom';
import { House } from './../types/house';
import { useMutation, useQuery, useQueryClient } from 'react-query';
import Config from '../config';
import axios, { AxiosError, AxiosResponse } from 'axios';
// import Problem from '../types/problem';
import { useEffect, useState } from 'react';

const useFetchHouses = () => {
	return useQuery<House[], AxiosError>('houses', () =>
		axios.get(`${Config.baseApiUrl}/houses`).then(res => res.data)
	);
};

const useFetchHouse = (id: number) => {
	return useQuery<House, AxiosError>(['houses', id], () =>
		axios.get(`${Config.baseApiUrl}/houses/${id}`).then(res => res.data)
	);
};

export default useFetchHouses;
export { useFetchHouse };
