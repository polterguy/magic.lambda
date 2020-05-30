
# Magic Lambda for .Net

[![Build status](https://travis-ci.org/polterguy/magic.lambda.svg?master)](https://travis-ci.org/polterguy/magic.lambda)

Magic Lambda is a microscopic Turing complete programming language based upon [Magic Node](https://github.com/polterguy/magic.node)
and [Magic Signals](https://github.com/polterguy/magic.signals). It provides the familiar _"keywords"_, such as **[for-each]**
and **[if]**, by exposing [Super Signal Slots](https://dzone.com/articles/super-signals-in-aspnet-core) for these keywords,
making them easily available for you in your Hyperlambda code. Althought technically not entirely true, this project is what allows Hyperlambda to become _"Turing complete"_, and gives you what most would consider to be a fully fledged _"programming language"_.

## Structure

Since _everything_ is a slot in Hyperlambda, this allows you to evaluate its conditional operators and logical operators,
the same way you would evaluate a function in a traditional programming language. This might at first seem a bit unintuitive
if you come from a traditional programming language, but has lots of advantages, such as allowing the computer to look
at the entirety of your function objects as hierarchical tree structures, parsing them as such, and imagining these as
_"execution trees"_.

For instance, in a normal programming language, the equal operator must have a left hand side (lhs), and a right hand
side (rhs). In Hyperlambda this is not true, since the equal slot is the main invocation of a function, requiring two
arguments, allowing you to think about it as a function. To compare this to the way a traditional programming might
have implemented this, imagine the equal operator as a function, such as the following pseudo code illustrates.

```javascript
equal(arg1, arg1)
```

The actual Hyperlambda code that would be the equivalent of the above pseudo code, can be found below.

```
eq
   .:arg1
   .:arg2
```

As you learn Hyperlambda, it might be beneficial to use the _"Evaluator"_ component that you can find in its
frontend Angular dashboard website. This component allows you to play with Hyperlambda in _"immediate mode"_,
allowing you to experiment with it, execute it immediately from your browser, using a very rich code editor,
providing syntax highlighting, autocomplete on slots, and allows you to save your snippets for later on your
server. If you do this, then click the _"information button"_ in the component to learn the basic shortcuts,
and access help in general. Below is a screenshot of the _"Evaluator"_ component to give you an idea of what
you might expect.

<img alt="Hyperlambda Evaluator" title="Hyperlambda Evaluator" src="https://servergardens.files.wordpress.com/2020/05/evaluator.png" />

## Slots

* __[if]__
* __[else]__
* __[else-if]__
* __[eq]__
* __[exists]__
* __[lt]__
* __[lte]__
* __[mt]__
* __[mte]__
* __[and]__
* __[or]__
* __[not]__
* __[switch]__
* __[case]__
* __[default]__
* __[add]__
* __[apply]__
* __[insert-after]__
* __[insert-before]__
* __[remove-node]__
* __[set-name]__
* __[set-value]__
* __[unwrap]__
* __[get-count]__
* __[get-name]__
* __[get-nodes]__
* __[get-value]__
* __[reference]__
* __[convert]__
* __[throw]__
* __[try]__
* __[for-each]__
* __[while]__
* __[eval]__
* __[vocabulary]__

### [if]

This is the Hyperlambda equivalent of `if` from other programming languages. It allows you to test for some condition,
and evaluate a lambda object, only if the condition evaluates to true. **[if]** must be given exactly two arguments.
The first argument can be anything, including a slot invocation - But its second argument must be its **[.lambda]**
argument. The **[.lambda]** node will be evaluated as a lambda object, only if the first argument to **[if]** evaluates
to boolean true. Below is an example.

```
.dest
if
   .:bool:true
   .lambda
      set-value:x:@.dest
         .:yup!
```

### [else-if]

**[else-if]** is the younger brother of **[if]**, and must be preceeded by its older brother, or other **[else-if]** nodes,
and will only be evaluated if all of its previous conditional slots evaluates to false - At which point **[else-if]** is
allowed to test its condition - And only if it evaluates to true, evaluate its lambda object. Semantically **[else-if]**
is similar to **[if]**,in that it requires exactly two arguments with the same structure as **[if]**.

```
.src:int:2
.dest
if
   eq
      get-value:x:@.src
      .:int:1
   .lambda
      set-value:x:@.dest
         .:yup!
else-if
   eq
      get-value:x:@.src
      .:int:2
   .lambda
      set-value:x:@.dest
         .:yup2.0!
```

### [else]

**[else]** is the last of the _"conditional siblings"_ that will only be evaluated as a last resort, only if none of its
elder _"siblings"_ evaluates to true. Notice, contrary to both **[if]** and **[else-if]**, **[else]** contains its lambda object
directly as children nodes, and _not_ within a **[.lambda]** node. This is because **[else]** does not require any
conditional arguments like **[if]** and **[else-if]** does. An example can be found below.

```
.src:int:3
.dest
if
   eq
      get-value:x:@.src
      .:int:1
   .lambda
      set-value:x:@.dest
         .:yup!
else-if
   eq
      get-value:x:@.src
      .:int:2
   .lambda
      set-value:x:@.dest
         .:yup2.0!
else
   set-value:x:@.dest
      .:nope
```

### [eq]

**[eq]** is the equality _"operator"_ in Magic, and it requires two arguments, both of which will be evaluated as potential
signals - And the result of evaluating **[eq]** will only be true if the values of these two arguments are _exactly the same_.
Notice, the comparison operator will consider types, which implies that boolean true will _not_ be considered equal to the string
value of _"true"_, etc.

```
.src:int:5
eq
   get-value:x:@.src
   .:int:5
```

### [exists]

**[exists]** will evaluate to true if its specified expression yields one or more results. If not, it will
return false.

```
.src1
   foo
.src2
exists:x:@.src1/*
exists:x:@.src2/*
```

### [lt]

**[lt]** will do a comparison between its two arguments, and only return true if its first argument is _"less than"_
its seconds argument. Consider the following.

```
.src1:int:4
lt
   get-value:x:@.src1
   .:int:5
```

### [lte]

**[lte]** will do a comparison between its two arguments, and only return true if its first argument is _"less than or equal"_
to its seconds argument. Consider the following.

```
.src1:int:4
lte
   get-value:x:@.src1
   .:int:4
```

### [mt]

**[mt]** will do a comparison between its two arguments, and only return true if its first argument is _"more than"_
its seconds argument. Consider the following.

```
.src1:int:7
mt
   get-value:x:@.src1
   .:int:5
```

### [mte]

**[mte]** will do a comparison between its two arguments, and only return true if its first argument is _"more than or equal"_
to its seconds argument. Consider the following.

```
.src1:int:7
mte
   get-value:x:@.src1
   .:int:5
```

### [and]

**[and]** requires two or more arguments, and will only evaluate to true, if all of its arguments evaluates to true. Consider
the following.

```
and
   .:bool:true
   .:bool:false
and
   .:bool:true
   .:bool:true
```

And will (of course) evaluate its arguments before checking if they evaluate to true, allowing you to use it as a part
of richer comparison trees, such as the following illustrates.

```
.s1:bool:true
.s2:bool:true
.res
if
   and
      get-value:x:@.s1
      get-value:x:@.s2
   .lambda
      set-value:x:@.res
         .:OK
```

### [or]

**[or]** is similar to **[and]**, except it will evaluate to true if _any_ of its arguments evaluates to true, such
as the following illustrates. **[or]** will also evaluate its arguments, allowing you to use it as a part of richer comparison
trees, the same way **[and]** allows you to. Below is a simple example.

```
or
   .:bool:false
   .:bool:false
or
   .:bool:false
   .:bool:true
```

### [not]

**[not]** expects _exactly one argument_, and will negate its boolean value, whatever it is, such as the following illustrates.

```
not
   .:bool:true
not
   .:bool:false
```

**[not]** will also evaluate its argument, allowing you to use it in richer comparison trees, the same you could do
with both **[or]** and **[and]**.

### [switch[]

**[switch]**

### [case]
### [default]
### [add]
### [apply]
### [insert-after]
### [insert-before]
### [remove-node]
### [set-name]
### [set-value]
### [unwrap]
### [get-count]
### [get-name]
### [get-nodes]
### [get-value]
### [reference]
### [convert]
### [throw]
### [try]
### [for-each]
### [while]
### [eval]
### [vocabulary]

## License

Although most of Magic's source code is publicly available, Magic is _not_ Open Source or Free Software.
You have to obtain a valid license key to install it in production, and I normally charge a fee for such a
key. You can [obtain a license key here](https://servergardens.com/buy/).
Notice, 5 hours after you put Magic into production, it will stop functioning, unless you have a valid
license for it.

* [Get licensed](https://servergardens.com/buy/)
