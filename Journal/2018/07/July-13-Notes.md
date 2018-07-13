# Journal

* Forwarded [Graph Data Structures for Beginners](https://adrianmejia.com/blog/2018/05/14/data-structures-for-beginners-graphs-time-complexity-tutorial/#List.space) - Adrian Mejia to Brian - 
* [The Spiral Language](https://github.com/mrakgr/The-Spiral-Language/) is a language written in F# that targets GPUs.
* [The System Design Primer](https://github.com/donnemartin/system-design-primer) - A study guide for designing large scalable systems.
  * Thoughts - Many of these patterns seem like they could be reified with Pulumi.

# Pairing Functions

This PDF has a nice defition of a [pairing function](http://www.lsi.upc.es/~alvarez/calculabilitat/enumerabilitat.pdf) defined as:

"A pairing function is a bijection between N x N and N that is also strictly monotone in each of its arguments.
If we let p: (N * N) -> N be a pairing function, we require:"

1. "p is bijection"
2. "p is structly monotone in each argument. forall(fun x y -> p(x, y) < p(x + 1, y) && p(x,y) < p(x, y + 1))


