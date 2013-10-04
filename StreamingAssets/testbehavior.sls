#!r6rs
;; Library for common unity helper functions. Very little here for now but will
;; be expanded.
(library (testbehavior)
         (export update)
         (import (rnrs)
                 (ironscheme clr))
         (clr-using UnityEngine)

(define (update)
  (clr-static-call Debug Log "Updating!"))
)