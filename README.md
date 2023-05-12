# GenShapeMakingKernelsC
Creating a number of kernels through code generation that allows clusters to be found in all shapes and sizes.

A cluster is a group of words that all connect together in a block like a 2x2 or 2x3 where every letter in the block is an intersection of two words, an interlock.

This code originally came from https://github.com/geurtjens/CrozzleCodeGen that generated code in swift but c++ is a lot faster to execute and 
c++ is easier to run on non apple machines.

A kernel is not necessarily something that runs on GPU in this case but is a single function that generates all the shapes for each combination of cluster.
