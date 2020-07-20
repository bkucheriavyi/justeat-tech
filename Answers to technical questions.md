## Technical questions
Q:How long did you spend on the coding test? What would you add to your solution if you had more time?
If you didn't spend much time on the coding test then use this as an opportunity to explain what you would add.

> I've spent ~6h, I would continue setting things up, adding more tests, SwaggerUI and OpenAPI integration, 
and React SPA - that was the reason why there is WebAPI project present, which I use as en endpoint from the console app.

Q: What was the most useful feature that was added to the latest version of your chosen language? 
Please include a snippet of code that shows how you've used it.

> c# 8 pattern matching, conditional catch clause (which was there for a while, so, not so new, but still very useful) and local functions - I just like to use them sometimes!

``` 
            public static void DoSomething()
            {
                static object CreateShape(string shapeDescription)
                {
                    switch (shapeDescription)
                    {
                        case var o when (o?.Trim().Length ?? 0) == 0:
                            return null;
                        default:
                            throw new ArgumentException("Hello World!!!");
                    }
                }

                try
                {
                    CreateShape("Not an empty shape");
                }
                catch (Exception ex) when (ex.Message == "Hello world!!!")
                {
                    Console.WriteLine("Hello from the pattern matching, local functions and custom exception handling");
                }
            }

```

Q: How would you track down a performance issue in production? Have you ever had to do this?

> So first and obvious - it's by hands, exploring and playing around with the application, 
but the more methodological way would be to have performance tests written and applied regularly alongside with metrics gathering configured (for statistics as well)
And to take a clear look at what is taking things so long - tracing, implemented and configured

Q: How would you improve the Just Eat APIs that you just used?

> I would very likely add offset/pagination for the data retrieval
