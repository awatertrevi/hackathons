(function () 
{
    $('<div class="input"><input placeholder="token" id="input_bearerToken" type="text" size="10"></div>').insertBefore('#api_selector div.input:last-child');
    $("#input_apiKey").hide();

	$('#input_bearerToken').change(function addAuthorization()
	{
		var customHeaders = 
		{
		    headerKey: new SwaggerClient.ApiKeyAuthorization("Authorization", "Bearer " + $("#input_bearerToken").val(), "header"),
		};

		swaggerUi.api.clientAuthorizations.add(customHeaders);

		console.log('Authorization header added.');
	});
})();
