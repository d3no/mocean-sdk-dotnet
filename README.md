MoceanAPI Client Library for C#/.NET
============================

This is the C# client library for use Mocean's API. To use this, you'll need a Mocean account. Sign up [for free at 
moceanapi.com][signup].

 * [Installation](#installation)
 * [Usage](#usage)
 * [Example](#example)

## Installation

To use the client library you'll need to have [created a Mocean account][signup]. 

To install the C# client library using NuGet:

 - Run the following command in the Package Manager Console:

```bash
Install-Package Mocean.Csharp.Client
```

## Usage

You need to create an object of Credential containing your API key and secret to work with all Client function:

```csharp
Credentials creds = new Credentials
{
    mocean_api_key = "API_KEY_HERE",
    mocean_api_secret = "API_SECRET_HERE"
};
```

## Example

To use [Mocean's SMS API][doc_sms] to send an SMS message, call the `mocean.sms.create().send()` method.

The API can be called directly, using a simple array of parameters, the keys match the [parameters of the API][doc_sms].

```csharp
Mocean.Message.Message _message = new Mocean.Message.Message()
{
    mocean_to = "60123456789",
    mocean_from = "MOCEAN",
    mocean_text = "Hello World",
    mocean_resp_format = "json",
};

string response = Mocean.Message.Client.Send(_message, creds);
```

## License

This library is released under the [MIT License][license]

[signup]: https://dashboard.moceanapi.com/register?medium=github&campaign=sdk-csharp
[doc_sms]: https://docs.moceanapi.com/?csharp#send-sms
[doc_inbound]: https://docs.moceanapi.com/?csharp#receive-sms
[doc_verify]: https://docs.moceanapi.com/?csharp#overview-3
[license]: LICENSE.md
