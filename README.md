# SalesTax
SalesTax

Sales Tax Example Web Site

There are 4 main classes. The Cart, Item, SalesTax, ImportTax. (Cart has Item has Tax)

The Cart contains Items and is used to output the item information, taxes, and total.

The Item class is used to hold attributes related to an Item.

The SalesTax and ImportTax classes are used to calculate the sales and import taxes. The tax classes load the tax percent from the appsettings file to allow the values to be easily changed.

The Test and Parser classes are used to load the examples and parse the input data.

The Sales/Import Tx classes could have easily been Abstract classes. However, in the really world usually tax calcuations are more complex.  In addition a currency class would be used instead of a deciamal for money.  

BTW, this is not how a would normally build a web site. I usually follow a service/micorService pattern. My controller are usually void of any type of buissness or data logic.

Note: Looks like the css is no longer working.
