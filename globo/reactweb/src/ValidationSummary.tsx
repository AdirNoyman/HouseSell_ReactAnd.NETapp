import { AxiosError } from 'axios';
import React from 'react';
import Problem from './types/problem';

type Args = {
	error: AxiosError<Problem>;
};

const ValidationSummary = ({ error }: Args) => {
	// If no error, return an empty object
	if (error.response?.status !== 400) return <></>;

	// Get the errors reurned from Axios
	const errors = error.response?.data.errors;

	// Present the errors
	return (
		<>
			<div className='text-danger'>Please fix the following:</div>
			{Object.entries(errors).map(([key, value]) => (
				<ul key={key}>
					<li>
						{/* Key is the name of the input field and the value is the validation errors */}
						{key}: {value.join(', ')}
					</li>
				</ul>
			))}
		</>
	);
};

export default ValidationSummary;
