import React from 'react';
import { useParams } from 'react-router-dom';
import ApiStatus from '../ApiStatus';
import { useFetchHouse, useUpdateHouse } from '../hooks/HouseHooks';
import HouseForm from './HouseForm';

const HouseEdit = () => {
	// find the house to update
	// take the id of the requested house from the url
	const { id } = useParams();

	// if no id was delivered by the user, throw an error message
	if (!id) throw Error('Please provide a house ID 🤨');

	const houseId = parseInt(id);

	const { data, status, isSuccess } = useFetchHouse(houseId);
	const updateHouseMutation = useUpdateHouse();

	if (!isSuccess) return <ApiStatus status={status} />;

	return (
		<HouseForm
			house={data}
			submitted={h => updateHouseMutation.mutate(h)}
		/>
	);
};

export default HouseEdit;
