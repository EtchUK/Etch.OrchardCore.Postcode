# Etch.OrchardCore.Postcode

Add postcode search functionality.

## Build Status
[![Build Status](https://secure.travis-ci.org/etchuk/Etch.OrchardCore.Postcode.png?branch=master)](http://travis-ci.org/etchuk/Etch.OrchardCore.Postcode) [![NuGet](https://img.shields.io/nuget/v/Etch.OrchardCore.Postcode.svg)](https://www.nuget.org/packages/Etch.OrchardCore.Postcode)

## Orchard Core Reference

This module is referencing the RC1 build of Orchard Core ([`1.0.0-rc2-13450`](https://www.nuget.org/packages/OrchardCore.Module.Targets/1.0.0-rc2-13450)).

## Installing

This module is available on NuGet. Add a reference to your Orchard Core web project via the NuGet package manager. Search for "Etch.OrchardCore.Postcode", ensuring include prereleases is checked.

## Usage

Enable "Postcode" feature within the admin dashboard. This will enable Postcode part to add to any content item and also adds the ability to create Postcode search page.

## Features

### Postcode Part

When enabled, a new "PostcodePart" can be attached to allow content editors to define a postcode for the content type, which will automatically fetch the longitude & latitude (via [https://api.postcodes.io/](https://api.postcodes.io/)) to be stored on the content item. By adding the "PostcodePart" to a content type, it will automatically be included in the search results for the postcode search.

### Postcode Search

When enabled, a new "Postcode Search" content type will be available. When a postcode search content item is created, the front-end will display a search form with a free text field and submit button. When submitting a postcode, the request will fetch the longitude & latitude for the postcode and then display the search results ordered by which ones are closest to the postcode search parameter.

To customise the search form & results create a template named "PostcodeSearch.List" within your custom theme or through the templates option within the admin dashboard.

## Packaging

When the theme is compiled (using `dotnet build`) it's configured to generate a `.nupkg` file (this can be found in `\bin\Debug\` or `\bin\Release`).