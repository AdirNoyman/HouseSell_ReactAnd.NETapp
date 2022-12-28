// import { useNavigate } from 'react-router-dom';
import { House } from './../types/house';
import { useMutation, useQuery, useQueryClient } from 'react-query';
import Config from '../config';
import axios, { AxiosError, AxiosResponse } from 'axios';
import { useNavigate } from 'react-router-dom';
// import Problem from '../types/problem';
// import { useEffect, useState } from 'react';

// Get all houses
const useFetchHouses = () => {
	// 'houses' will be the name of the cache
	return useQuery<House[], AxiosError>('houses', () =>
		axios.get(`${Config.baseApiUrl}/houses`).then(res => res.data)
	);
};

// Get one house
const useFetchHouse = (id: number) => {
	return useQuery<House, AxiosError>(['houses', id], () =>
		axios.get(`${Config.baseApiUrl}/houses/${id}`).then(res => res.data)
	);
};

// Create a new house
const useAddHouse = () => {
	const navigateTo = useNavigate();
	// Get the app query cache
	const queryClient = useQueryClient();
	return useMutation<AxiosResponse, AxiosError, House>(
		house => axios.post(`${Config.baseApiUrl}/houses`, house),
		{
			onSuccess: () => {
				// Invalidate the cache (causing the fetch houses to be revoked so the cache will be refreshed)
				queryClient.invalidateQueries('houses');
				// Present the home screen refreshed after the adding of the new house
				navigateTo('/');
			},
		}
	);
};

// Update a house
const useUpdateHouse = () => {
	const navigateTo = useNavigate();
	// Get the app query cache
	const queryClient = useQueryClient();
	return useMutation<AxiosResponse, AxiosError, House>(
		house => axios.put(`${Config.baseApiUrl}/houses`, house),
		{
			// The first parameter is the axios response object, that we are not using in this instance, and that is why we put '_'. we are intrested only in the house instance we are updating.
			onSuccess: (_, house) => {
				// Invalidate the cache (causing the fetch houses to be revoked so the cache will be refreshed)
				queryClient.invalidateQueries('houses');
				// Present the home screen refreshed after the adding of the new house
				navigateTo(`/houses/${house.id}`);
			},
		}
	);
};
// Delete a house
const useDeleteHouse = () => {
	const navigateTo = useNavigate();
	// Get the app query cache
	const queryClient = useQueryClient();
	return useMutation<AxiosResponse, AxiosError, House>(
		house => axios.delete(`${Config.baseApiUrl}/houses/${house.id}`),
		{
			onSuccess: () => {
				// Invalidate the cache (causing the fetch houses to be revoked so the cache will be refreshed)
				queryClient.invalidateQueries('houses');
				// Present the home screen refreshed after the deleting the house
				navigateTo('/');
			},
		}
	);
};

export default useFetchHouses;
export { useFetchHouse, useAddHouse, useUpdateHouse, useDeleteHouse };
