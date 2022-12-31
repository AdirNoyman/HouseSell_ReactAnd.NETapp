import { Bid } from './../types/bid';
import { useMutation, useQuery, useQueryClient } from 'react-query';
import Config from '../config';
import axios, { AxiosError, AxiosResponse } from 'axios';
import Problem from '../types/problem';

const useFetchBids = (houseId: number) => {
	return useQuery<Bid[], AxiosError>(['bids', houseId], () =>
		axios
			.get(`${Config.baseApiUrl}/houses/${houseId}/bids`)
			.then(resp => resp.data)
	);
};

const useAddBid = () => {
	const queryClient = useQueryClient();
	return useMutation<AxiosResponse, AxiosError<Problem>, Bid>(
		b => axios.post(`${Config.baseApiUrl}/houses/${b.houseId}/bids`, b),
		{
			// The first parameter is the axios response object, that we are not using in this instance, and that is why we put '_'. we are intrested only in the bid instance we are adding to the house's bids.
			onSuccess: (_, bid) => {
				queryClient.invalidateQueries(['bids', bid.houseId]);
			},
		}
	);
};

export { useFetchBids, useAddBid };
