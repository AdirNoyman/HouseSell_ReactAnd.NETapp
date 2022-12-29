import React from 'react';
import { useAddHouse } from '../hooks/HouseHooks';
import { House } from '../types/house';
import ValidationSummary from '../ValidationSummary';
import HouseForm from './HouseForm';

const HouseAdd = () => {
	const addHouseMutation = useAddHouse();

	const house: House = {
		address: '',
		country: '',
		description: '',
		price: 0,
		photo: '',
		id: 0,
	};

	return (
		<>
			{/* If the mutation of the cache has returned an error, display the ValidationSummary component */}
			{addHouseMutation.isError && (
				<ValidationSummary error={addHouseMutation.error} />
			)}
			<HouseForm
				house={house}
				submitted={h => addHouseMutation.mutate(h)}
			/>
		</>
	);
};

export default HouseAdd;
