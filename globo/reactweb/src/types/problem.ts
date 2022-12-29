type Error = {
	[name: string]: string[];
};

// This is the standard error structure for API errors
type Problem = {
	type: string;
	title: string;
	status: number;
	errors: Error;
};

export default Problem;
